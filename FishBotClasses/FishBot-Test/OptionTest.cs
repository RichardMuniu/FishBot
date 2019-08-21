using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FishBot
{
    /*
    * Testing strategy
    *
    * Partition the inputs as follows:
    * A) For Constructor:
    *   1) Name is empty.
    *   2) Name is null.
    *   3) Name is invalid.
    *   4) Value is null.
    *   5) Name and value are both valid.
    * B) For Property Setters and Getters:
    *   1) Setting name to null, empty or invalid one.
    *   2) Setting value to null.
    *   3) Getting the correct name and value.
    *   4) Setting name and value to valid ones.
    * C) For Similar:
    *   1) Options are similar.
    *   2) Options are not similar.
    *   3) Other option is null.
    *   
    *   Cover each part testing
    */ 
    [TestClass]
    public class OptionTest
    {
        // covers A1
        [TestMethod]
        public void NameEmpty()
        {
            string name = "";
            object value = new object();
            try{ Option op = new Option(name,value);}
            catch{ return; }
            Assert.Fail();
        }

        // covers A2
        [TestMethod]
        public void NameNull()
        {
            string name = null;
            object value = new object();
            try{ Option op = new Option(name,value);}
            catch{ return; }
            Assert.Fail();
        }

        // covers A3
        [TestMethod]
        public void NameInvalid()
        {
            string name = "hello hotmail";
            object value = new object();
            try{ Option op = new Option(name,value);}
            catch{ return; }
            Assert.Fail();
        }

        // covers A4
        [TestMethod]
        public void ValueNull()
        {
            string name = "hellohotmail";
            object value = null;
            try{ Option op = new Option(name,value);}
            catch{ return; }
            Assert.Fail();
        }

        // covers A5
        [TestMethod]
        public void NameValidValueValid()
        {
            string name = "hellohotmail";
            int value = 3;
            Option op = new Option(name,value);
        }

        // covers B1
        [TestMethod]
        public void SetNameToInvalid()
        {
            string name1 = "hello hotmail";
            string name2 = "";
            string name3 = null;
            int value = 3;
            bool pass = true;
            
            try{Option op = new Option(name1,value); pass = false;}
            catch{}
            try{Option op = new Option(name2,value); pass = false;}
            catch{}
            try{Option op = new Option(name3,value); pass = false;}
            catch{}
            
            if(pass == false)
                Assert.Fail();
        }

        // covers B2
        [TestMethod]
        public void SetValueToInvalid()
        {
            string name1 = "hellohotmail";
            int value = 3;
            Option op = new Option(name1,value);
            try{op.Value = null;}
            catch{return;}
            Assert.Fail();
        }

        // covers B3
        [TestMethod]
        public void GettersTest()
        {
            string name1 = "hellohotmail";
            int value = 3;
            Option op = new Option(name1,value);
            Assert.AreEqual(op.Value, 3);
            Assert.AreEqual(op.Name, "hellohotmail");
        }

        // covers B4
        [TestMethod]
        public void SettersTest()
        {
            string name1 = "hellohotmail";
            int value = 3;
            Option op = new Option(name1,value);
            op.Name = "hi";
            op.Value = 0;
            Assert.AreEqual(op.Value, 0);
            Assert.AreEqual(op.Name, "hi");
        }

        // covers C1
        [TestMethod]
        public void SimilarTrue()
        {
            string name1 = "hellohotmail";
            int value = 3;
            Option op = new Option(name1,value);
            Option op2 = new Option(name1, value);
            Assert.IsTrue(op.Similar(op2) && op2.Similar(op));
        }

        // covers C2
        [TestMethod]
        public void SimilarFalse()
        {
            string name1 = "hellohotmail";
            int value = 3;
            Option op = new Option(name1,value);
            Option op2 = new Option(name1 + ".", value);
            Assert.IsTrue(!op.Similar(op2) && !op2.Similar(op));
            Option op3 = new Option(name1, 4);
            Assert.IsTrue(!op3.Similar(op) && !op.Similar(op3));
        } 

        // covers C3
        [TestMethod]
        public void SimilarNull()
        {
            string name1 = "hellohotmail";
            int value = 3;
            Option op = new Option(name1,value);
            Option op2 = null;
            Assert.IsTrue(!op.Similar(op2));
        }             
    }
}
