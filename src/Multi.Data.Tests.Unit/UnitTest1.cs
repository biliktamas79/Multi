using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Multi.Data.Tests.Unit
{
    [TestClass]
    public class UnitTest1
    {
        private class Contact : IReadOnlyPkHolder<int>
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }

            public int GetPk()
            {
                return Id;
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            //IEntityRepo<int, Contact> repo = null;
            //// TODO initialize repo
            //repo.Get(
            //    c => c.FirstName == "John",
            //    new OrderBy[]
            //    {
            //        new OrderBy(nameof(Contact.LastName), OrderEnum.ASC),
            //        new OrderBy(nameof(Contact.Id), OrderEnum.ASC),
            //    },
            //    0, 1);
        }
    }
}
