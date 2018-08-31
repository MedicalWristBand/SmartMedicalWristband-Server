using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoliceServer.Utilities;

namespace PoliceServer.Models
{
    public class AbstractEntity : ICloneable
    {
        protected readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public AbstractEntity()
        {
            this.Id = System.Guid.NewGuid().GetHashCode();
        }


        public object Clone()
        {
            PoliceContext contex = ContextCreator.GetInstance().GetContext();
            DbSet set = contex.Set(this.GetType());
            AbstractEntity clonedEntity = set.Find(this.Id) as AbstractEntity;
            contex.Entry(clonedEntity).State = EntityState.Detached;
            if (clonedEntity == null)
            {
                //TODO
                //throw new UserInterfaceException(27701, String.Format("امکان کپی کردن {0} با شناسه ی {1} وجود ندارد، لطفا با بخش پشتیبانی تماس بگیرید.", this.GetType(), this.ID));
                throw new Exception("----abstract entity failed! ----");
            }
            clonedEntity.Id = System.Guid.NewGuid().GetHashCode();
            return clonedEntity;
        }
    }
}