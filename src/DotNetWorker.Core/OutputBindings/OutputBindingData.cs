﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Azure.Functions.Worker
{
    /// <summary>
    /// A type representing the output binding data.
    /// </summary>
    public sealed class OutputBindingData<T>
    {
        internal OutputBindingData(FunctionContext functionContext, string name, T? value, string bindingType)
        {
            _functionContext = functionContext;
            _value = value;
            Name = name;
            BindingType = bindingType;
        }

        private T? _value;
        private readonly FunctionContext _functionContext;

        /// <summary>
        /// Gets the name of the binding entry.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the value of the binding entry.
        /// </summary>
        public T? Value
        {
            get => _value;
            set
            {
                _value = value;
                _functionContext.GetBindings().OutputBindingData[Name] = value;
            }
        }

        /// <summary>
        /// Gets the type of the binding entry.
        /// Ex: "http","queue" etc.
        /// </summary>
        public string BindingType { get; }
    }
}