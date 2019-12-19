using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Serialization;
using WebSvc1.Models;

namespace WebSvc1.Repositories
{
    public class BonusRepository : IBonusRepository
    {
        public async Task<bool> SaveAsync(IList<EmployeeBonus> employeeBonusList)
        {
            bool result = false;
            await Task.Run(() => { result = SerializeObject(employeeBonusList); }).ConfigureAwait(false);
            return result;
        }

        private bool SerializeObject<T>(T serializableObject)
        {
            bool result = false;
            if (serializableObject == null)
                return result;

            XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
            using (XmlWriter xmlWriter = XmlWriter.Create(HostingEnvironment.MapPath(@"~/App_Data/EmployeeBonus.xml")))
            {
                serializer.Serialize(xmlWriter, serializableObject);
                xmlWriter.Flush();
                xmlWriter.Close();
                result = true;
            }

            return result;
        }
    }
}