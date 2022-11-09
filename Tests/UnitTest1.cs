using ITLab.Classes.Database;
using ITLab.Classes.Database.Columns;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void IntValidationTest()
        {
            string wrongVal = "a", rightVal = "1";
            IntCol intCol = new IntCol("intCol");
            Assert.False(intCol.Validate(wrongVal));
            Assert.True(intCol.Validate(rightVal));
        }
        [Fact]
        public void RealValidationTest()
        {
            string wrongVal = "a", rightVal1 = "1", rightVal2 = "1,5";
            RealCol realCol = new RealCol("realCol");
            Assert.False(realCol.Validate(wrongVal));
            Assert.True(realCol.Validate(rightVal1));
            Assert.True(realCol.Validate(rightVal2));
        }
        [Fact]
        public void StringValidationTest()
        {
            string rightVal1 = "fff";
            StrCol strCol = new StrCol("strCol");
            Assert.True(strCol.Validate(rightVal1));
        }
        [Fact]
        public void CharValidationTest()
        {
            string wrongVal = "aaaa", rightVal1 = "x";
            CharCol charCol = new CharCol("charCol");
            Assert.False(charCol.Validate(wrongVal));
            Assert.True(charCol.Validate(rightVal1));
        }
        [Fact]
        public void EmailValidationTest()
        {
            string wrongVal = "Hi, Mark", rightVal1 = "danilo.zotov@gmail.com";
            EmailCol emailCol = new EmailCol("charCol");
            Assert.False(emailCol.Validate(wrongVal));
            Assert.True(emailCol.Validate(rightVal1));
        }
        [Fact]
        public void SetValidationTest()
        {
            string wrongVal = "e", rightVal1 = "a";
            SetCol setCol = new SetCol("charCol", "a;b;c;d");
            Assert.False(setCol.Validate(wrongVal));
            Assert.True(setCol.Validate(rightVal1));
        }
        [Fact]
        public void MultiplyTest()
        {
            DatabaseManager.Instance.CreateDatabase("test");
            DatabaseManager.Instance.AddTable("one");
            DatabaseManager.Instance.AddTable("two");
            DatabaseManager.Instance.AddTable("validate");
            DatabaseManager.Instance.AddColumn("one", "a", "String");
            DatabaseManager.Instance.AddColumn("one", "b", "String");
            DatabaseManager.Instance.AddRow("one", "x;y");
            DatabaseManager.Instance.AddColumn("two", "c", "String");
            DatabaseManager.Instance.AddRow("two", "z");
            DatabaseManager.Instance.AddRow("two", "w");
            DatabaseManager.Instance.AddColumn("validate", "a", "String");
            DatabaseManager.Instance.AddColumn("validate", "b", "String");
            DatabaseManager.Instance.AddColumn("validate", "c", "String");
            DatabaseManager.Instance.AddRow("validate", "x;y;z");
            DatabaseManager.Instance.AddRow("validate", "x;y;w");

            Table validate = DatabaseManager.Instance.Database.GetTable("validate");
            Table res = DatabaseManager.Instance.Multiply("one", "two");

            Assert.NotNull(res);
            Assert.Equal(validate.Rows.Count, res.Rows.Count);
            Assert.Equal(validate.Columns.Count, res.Columns.Count);
            Assert.Equal(validate.Rows[0].Values[0], res.Rows[0].Values[0]);
            Assert.Equal(validate.Rows[0].Values[1], res.Rows[0].Values[1]);
            Assert.Equal(validate.Rows[0].Values[2], res.Rows[0].Values[2]);
            Assert.Equal(validate.Rows[1].Values[0], res.Rows[1].Values[0]);
            Assert.Equal(validate.Rows[1].Values[1], res.Rows[1].Values[1]);
            Assert.Equal(validate.Rows[1].Values[2], res.Rows[1].Values[2]);

        }


    }
}