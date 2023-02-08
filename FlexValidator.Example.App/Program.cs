using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FlexValidator.Example.App.Models;
using FlexValidator.Example.App.Validators;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FlexValidator.Example.App {
    class Program {
        static void Main(string[] args) {
            var model = CreateModel();
            Console.WriteLine();

            var task = ValidateModelAsync(model);
            var result = task.Result;

            LogResults(result);
        }

        private static SomeModel CreateModel() {
            var model = new SomeModel() {
                Id = -5,
                Name = "didii",
                Sub = new SubModel() {
                    Id = 0,
                    Name = "",
                    Type = SubModelType.Allowed
                },
                DoubleLeft = new DoubleModel() {
                    Name = "A name",
                    Type = DoubleModelType.In
                },
                DoubleRight = new DoubleModel() {
                    Name = "Another name",
                    Type = DoubleModelType.In
                }
            };
            Console.WriteLine("Using the following model:");
            Console.WriteLine(JsonConvert.SerializeObject(model, new JsonSerializerSettings() {
                Formatting = Formatting.Indented,
                Converters = { new StringEnumConverter() }
            }));
            return model;
        }

        private static async Task<IValidationResult> ValidateModelAsync(SomeModel model) {
            var validator = new SomeModelValidator();
            return await validator.ValidateAsync(model);
        }

        private static void LogResults(IValidationResult result) {
            if (result.IsValid) {
                Console.WriteLine("Validation succeeded");
                return;
            }
            foreach (var fail in result.Fails)
                Console.WriteLine($"{fail.Id}: {fail.Message}");
        }
    }
}