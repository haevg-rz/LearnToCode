using System;
using System.Collections.Generic;
using System.Linq;

namespace Mock
{
    class Program
    {
        static void Main(string[] args)
        {
            // Production
            {
                DbReader dbReader = new DbReader();
                // Note: Throws Exception because build and test server should not connect to datebase
                // var lastEntriesProd = dbReader.GetLastEntities();
            }
            
            // Test
            {
                DbReader dbReaderTest = new DbReader();
                dbReaderTest.DbAccess = new DbAccessTest();
                var lastEntriesTest = dbReaderTest.GetLastEntities();

                if (lastEntriesTest.Count() != 100)
                {
                    throw new Exception($"{lastEntriesTest.Count()} should be 100");
                }

                if (dbReaderTest.GetConnectionString() != "TEST")
                {
                    throw new Exception($"{dbReaderTest.GetConnectionString()} should be TEST");
                }
            }

            // Test with Mock
            {
                DbReader dbReaderTest = new DbReader();
                var mock = new Moq.Mock<IDbAccess>();
                mock.Setup(m => m.GetConnectionString()).Returns("TEST");
                mock.Setup(m => m.GetLastEntities()).Returns(Enumerable.Range(0, 100).Select(i => new Entity {Id = i}));

                dbReaderTest.DbAccess = mock.Object;
                var lastEntriesTest = dbReaderTest.GetLastEntities();

                if (lastEntriesTest.Count() != 100)
                {
                    throw new Exception($"{lastEntriesTest.Count()} should be 100");
                }

                if (dbReaderTest.GetConnectionString() != "TEST")
                {
                    throw new Exception($"{dbReaderTest.GetConnectionString()} should be TEST");
                }
            }
        }
    }

    public class DbReader
    {
        public IDbAccess DbAccess { get; set; } = new DbAccess();

        public IEnumerable<Entity> GetLastEntities()
        {
            return DbAccess.GetLastEntities();
        }

        public string GetConnectionString()
        {
            return DbAccess.GetConnectionString();
        }
    }

    public interface IDbAccess
    {
        IEnumerable<Entity> GetLastEntities();
        string GetConnectionString();
    }

    public class DbAccess : IDbAccess
    {
        public IEnumerable<Entity> GetLastEntities()
        {
            // Sql Commands etc.
            throw new NotImplementedException();
        }

        public string GetConnectionString()
        {
            return "PROD";
        }

        public double SeverLoad()
        {
            return 0.5;
        }
    }

    public class DbAccessTest : IDbAccess
    {
        public IEnumerable<Entity> GetLastEntities()
        {
            return Enumerable.Range(0, 100).Select(i => new Entity {Id = i});
        }

        public string GetConnectionString()
        {
            return "TEST";
        }
    }

    public class Entity
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}: {this.Id}";
        }
    }
}