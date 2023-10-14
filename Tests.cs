using TechnicalAssesment.POMPages;
using IronXL;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System;

namespace TechnicalAssesment
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Question1()
        {
            string fileLocation = "";
            using (var reader = new StreamReader("filePersons.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Person>();
            }

            Assert.Pass();
        }

    }
}