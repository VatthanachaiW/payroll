using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payroll.IBaseAdhocs;

namespace Payroll.Connections.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DbContextExtensions
    {
        const string DynamicProxy = "DynamicProxy";
        const string Castle = "Castle";
        const string MeetingRoomRepository = "MeetingRoom.Repositories";

        const string DeclaringNameRemover = "+";
        const string FullMethodNameRemover = "Void MoveNext()";
        const string NameRemover = "MoveNext";


        public static async Task<TEntity> AddEntityAsync<TEntity>(this DbContext dbContext, TEntity entity)
            where TEntity : class
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public static TEntity RemoveEntity<TEntity>(this DbContext dbContext, TEntity entity) where TEntity : class
        {
            dbContext.Set<TEntity>().Remove(entity);
            return entity;
        }

        public static List<TEntity> RemoveRangeEntity<TEntity>(this DbContext dbContext, List<TEntity> entity)
            where TEntity : class
        {
            dbContext.Set<TEntity>().RemoveRange(entity);
            return entity;
        }

        public static async Task<int> CommitAsync(this DbContext dbContext)
        {
            //Get the Stack Trace all frames.
            var methodBases = (new System.Diagnostics.StackTrace().GetFrames() ?? throw new InvalidOperationException())
                .Select(i => i.GetMethod())
                .ToList(); //Getting StackTrace
            methodBases = methodBases //Filter by removing Proxy and Castle library.
                .Where(i => i.DeclaringType?.Namespace != null &&
                            i.DeclaringType?.Namespace.IndexOf(DynamicProxy, StringComparison.Ordinal) <=
                            -1) //Remove the DynamicProxy at Declaring type
                .Where(i => i.ToString().IndexOf(DynamicProxy, StringComparison.Ordinal) <=
                            -1) //Remove the DynamicProxy at current type
                .Where(i => i.DeclaringType?.Namespace != null &&
                            i.DeclaringType?.Namespace.IndexOf(Castle, StringComparison.Ordinal) <= -1)
                .Where(i => i.DeclaringType.FullName != null &&
                            i.DeclaringType.FullName.IndexOf(DeclaringNameRemover, StringComparison.Ordinal) <=
                            -1) //Remove the DeclaringName that contains with " + "
                .Where(i => i.ToString().IndexOf(FullMethodNameRemover, StringComparison.Ordinal) <=
                            -1) //Remove the Full Method Name that contains " Void MoveNext() "
                .Where(i => i.DeclaringType != null &&
                            i.DeclaringType.Name.IndexOf(NameRemover, StringComparison.Ordinal) <=
                            -1) //Remove the Name that contains " MoveNext "
                .ToList(); //Remove the Castle Windsor DLL at Declaring type

            var selectMethodNames = methodBases.Select(i => new
            {
                //Populate the new list of objects
                i.DeclaringType?.Namespace,
                //i.Name, DeclaringName = i.ReflectedType?.FullName, FullMethodName = i.ToString()
            }).ToList();

            var count = methodBases.Count - 2; //Remove Presentation Layer
            count = count <= 0 ? 0 : count; //if layer is lester than 0 replace by zero.
            var isAsync = methodBases.Take(count)
                .Any(i => i.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) !=
                          null); //Check is called by async/await


            var findRepoCount = selectMethodNames.Count(i =>
                i.Namespace.IndexOf(MeetingRoomRepository, StringComparison.Ordinal) > -1);

            //Check the root of commit: [True is the root of commit, False is not the root of commit]
            if (findRepoCount <= (isAsync ? 2 : 1) && findRepoCount > 0)
            {
                return await CommitInternal(dbContext);
            }

            //Return 0 because this is no the root of commit;
            return 0;
        }

        public static async Task<int> CommitAsync(this DbContext dbContext, object executeBy) =>
            executeBy switch
            {
                null => throw new ArgumentNullException(nameof(executeBy)),
                ICommitAble _ => await CommitInternal(dbContext),
                _ => 0
            };

        static async Task<int> CommitInternal(DbContext dbContext)
        {
            await dbContext.Database.BeginTransactionAsync();
            try
            {
                var result = await dbContext.SaveChangesAsync();
                dbContext.Database.CommitTransaction();
                return result;
            }
            catch (Exception)
            {
                dbContext.Database.RollbackTransaction();
                throw;
            }
        }

        public static IList<T> MapToList<T>(this DbDataReader reader)
        {
            var result = new List<T>();
            var props = typeof(T).GetRuntimeProperties();
            var colMapping = reader.GetColumnSchema()
                .Where(x => props.Any(y =>
                    y.Name.IndexOf(x.ColumnName, StringComparison.InvariantCultureIgnoreCase) > -1))
                .ToDictionary(key => key.ColumnName.ToLower());
            if (reader.HasRows)
            {
                var propertyInfos = props.ToList();
                while (reader.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in propertyInfos)
                    {
                        var columnOrdinal = colMapping[prop.Name.ToLower()].ColumnOrdinal;

                        if (columnOrdinal != null)
                        {
                            var val = reader.GetValue(columnOrdinal.Value);
                            prop.SetValue(obj, val == DBNull.Value ? null : val);
                        }
                    }

                    result.Add(obj);
                }
            }

            return result;
        }
    }
}