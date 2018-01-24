using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;

namespace Audit.Core.Channels.Storage
{
    public class NoSql : IChannel
    {
        private static readonly string ComsosConnectionString = ConfigurationManager.AppSettings["CosmosTableConnectionString"];
        private static readonly string AuditTable = ConfigurationManager.AppSettings["CosmoAuditTable"];

        public Task<bool> WriteAsync(Models.Audit data)
        {
            throw new NotImplementedException();
        }

        private static CloudTableClient CosmosClient()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ComsosConnectionString);
            TableConnectionPolicy tableConnectionPolicy = new TableConnectionPolicy();
            tableConnectionPolicy.EnableEndpointDiscovery = true;
            tableConnectionPolicy.UseDirectMode = true;
            tableConnectionPolicy.UseTcpProtocol = true;
            return storageAccount.CreateCloudTableClient(tableConnectionPolicy, Microsoft.Azure.CosmosDB.ConsistencyLevel.Session);
        }
    }
}