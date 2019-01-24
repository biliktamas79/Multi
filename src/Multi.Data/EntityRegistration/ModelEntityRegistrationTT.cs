using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Multi.Data.EntityRegistration
{
    using UnitOfWork;

    /// <summary>
    /// Class for model entity registrations of entity types having primary key
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key</typeparam>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public class ModelEntityRegistration<TPrimaryKey, TEntity> : ModelEntityRegistrationBase
        where TEntity : IReadOnlyPkHolder<TPrimaryKey>
    {
        /// <summary>
        /// Read-only field for the type of the primary key.
        /// </summary>
        public readonly Type PkType;
        /// <summary>
        /// Read-only field for the expression that gets the primary key from an entity instance.
        /// </summary>
        public readonly Expression<Func<TEntity, TPrimaryKey>> GetEntityPkDelegate;
        /// <summary>
        /// Read-only field for the expression that initializes a new entity instance with the given primary key.
        /// </summary>
        public readonly Expression<Func<TPrimaryKey, TEntity>> NewEntityWithPkFactory;
        /// <summary>
        /// Read-only field for the expression that returns a new primary key.
        /// </summary>
        public readonly Expression<Func<TPrimaryKey>> NewPkFactory;
        /// <summary>
        /// Read-only field for the delegate that initializes a new entity reference instance with the given primary key.
        /// </summary>
        public readonly Func<IUnitOfWorkBase, TPrimaryKey, EntityReferenceByPk<TPrimaryKey, TEntity>> NewEntityReferenceByPkFactory;
        /// <summary>
        /// Read-only field for the delegate that is used for checking equality of primary key values.
        /// </summary>
        public readonly Func<TPrimaryKey, TPrimaryKey, bool> CheckPkEqualityDelegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelEntityRegistration{TPrimaryKey, TEntity}"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="getEntityPkDelegate">The get entity pk delegate.</param>
        /// <param name="newEntityWithPkFactory">The new entity with pk factory.</param>
        /// <param name="newPkFactory">The new pk factory.</param>
        /// <param name="newEntityReferenceByPkFactory">The new entity reference by pk factory.</param>
        public ModelEntityRegistration(string tableName
            , Expression<Func<TEntity, TPrimaryKey>> getEntityPkDelegate
            , Func<TPrimaryKey, TPrimaryKey, bool> checkPkEqualityDelegate
            , Expression<Func<TPrimaryKey, TEntity>> newEntityWithPkFactory
            , Expression<Func<TPrimaryKey>> newPkFactory
            , Func<IUnitOfWorkBase, TPrimaryKey, EntityReferenceByPk<TPrimaryKey, TEntity>> newEntityReferenceByPkFactory
            )
            : base(tableName, typeof(TEntity))
        {
            Throw.IfArgumentIsNull(getEntityPkDelegate, nameof(getEntityPkDelegate));
            Throw.IfArgumentIsNull(checkPkEqualityDelegate, nameof(checkPkEqualityDelegate));
            Throw.IfArgumentIsNull(newEntityWithPkFactory, nameof(newEntityWithPkFactory));
            Throw.IfArgumentIsNull(newPkFactory, nameof(newPkFactory));
            Throw.IfArgumentIsNull(newEntityReferenceByPkFactory, nameof(newEntityReferenceByPkFactory));

            this.PkType = typeof(TPrimaryKey);
            this.GetEntityPkDelegate = getEntityPkDelegate;
            this.CheckPkEqualityDelegate = checkPkEqualityDelegate;
            this.NewEntityWithPkFactory = newEntityWithPkFactory;
            this.NewPkFactory = newPkFactory;
            this.NewEntityReferenceByPkFactory = newEntityReferenceByPkFactory;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.GetType().GetFriendlyTypeName()} to table '{TableName}'.";
        }
    }
}
