using MongoDB.Driver;

namespace DevTeamUtils.Api.Controllers
{
    public static class ControllersUtils
    {
        public static IMongoDatabase GetDatabase(MongoClient client)
        {
            //string connectionString = "mongodb://localhost:27017";
            //MongoClient client = new MongoClient(connectionString);
            IsConnected(client);
            return client.GetDatabase("DevTeamUtils");
        }

        public static MongoClient GetMongoClient(string connectionString)
        {
            return new MongoClient(connectionString);
        }

        public static bool IsConnected(MongoClient client)
        {
            //var server = client.GetServer();
            return false;
        }
    }
}
