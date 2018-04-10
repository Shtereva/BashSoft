﻿using System;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;

        protected Command(string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
        }

        protected string Input
        {
            get => this.input;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.input = value;
            }
        }

        protected string[] Data
        {
            get => this.data;
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }
                this.data = value;
            }
        }

        public abstract void Execute();
    }
}
