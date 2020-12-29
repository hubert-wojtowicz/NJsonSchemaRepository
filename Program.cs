using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NJsonSchemaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var schema = JsonSchema.FromType(typeof(bool));
            var schemaBook = JsonSchema.FromType(typeof(Book));
            var schemaString = JsonSchema.FromType(typeof(string));
            var schemaPhone = JsonSchema.FromType(typeof(Contact));
            var schemaPhoneJson = schemaPhone.ToJson();

            var phoneJson = JsonConvert.SerializeObject(new Contact { Number = "+48-740-343-234", Email ="upa@upa.com", Website = "http://google.pl" });
            var validator = new JsonSchemaValidator();
            var v = validator.Validate(phoneJson, schemaPhone);

            foreach (var error in schemaPhone.Validate(phoneJson))
            {
                Console.WriteLine(error.Path + ": " + error.Kind);
            }
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Book> ReferenceBooks { get; set; }
    }


    public class Contact
    {
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")]
        public string Number { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }
    }
}
