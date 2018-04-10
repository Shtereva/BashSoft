using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BashSoft.Contracts;
using BashSoft.Contracts.Repo.Database;
using BashSoft.IO.Attributes;
using BashSoft.IO.Commands;

namespace BashSoft
{
    public class CommandInterpreter : IInterpreter
    {
        private IContentComparer judge;
        private IDatabase repository;
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpredCommand(string input)
        {
            var data = input.Split();
            string commandName = data[0];

            try
            {
                IExecutable command = this.ParseCommand(input, data, commandName);
                command.Execute();

            }
            catch (DirectoryNotFoundException dnfe)
            {
                OutputWriter.DisplayException(dnfe.Message);
            }
            catch (ArgumentOutOfRangeException aoere)
            {
                OutputWriter.DisplayException(aoere.Message);
            }
            catch (ArgumentNullException ane)
            {
                OutputWriter.DisplayException(ane.Message);
            }
            catch (ArgumentException ae)
            {
                OutputWriter.DisplayException(ae.Message);
            }
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        private IExecutable ParseCommand(string input, string[] data, string command)
        {
            var constructorParameters = new object[] { input, data };

            Type typeOfCommand = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.GetCustomAttributes<AliasAttribute>()
                    .Where(a => a.Equals(command))
                    .ToArray().Length > 0);

            var interpreterType = typeof(CommandInterpreter);

            Command exe = (Command) Activator.CreateInstance(typeOfCommand, constructorParameters);

            var fieldsOfCommand = typeOfCommand
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            var fieldsOfInterpreter = interpreterType
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var fieldOfCommand in fieldsOfCommand)
            {
                var attribute = fieldOfCommand.GetCustomAttribute<InjectAttribute>();

                if (attribute != null)
                {
                    if (fieldsOfInterpreter.Any(f => f.FieldType == fieldOfCommand.FieldType))
                    {
                        var fieldValueOfInterpreter =
                            fieldsOfInterpreter.First(f => f.FieldType == fieldOfCommand.FieldType)
                            .GetValue(this);

                        fieldOfCommand.SetValue(exe, fieldValueOfInterpreter);
                    }
                }
            }
            return exe;
        }
    }
}
