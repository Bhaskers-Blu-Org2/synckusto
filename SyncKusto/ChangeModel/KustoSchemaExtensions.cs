﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Kusto.Data.Common;

namespace SyncKusto.ChangeModel
{
    public static class KustoSchemaExtensions
    {
        public static IKustoSchema AsKustoSchema(this TableSchema schema) => new KustoTableSchema(schema);
        public static IKustoSchema AsKustoSchema(this FunctionSchema schema) => new KustoFunctionSchema(schema);

        public static Dictionary<string, IKustoSchema> AsKustoSchema(this Dictionary<string, TableSchema> schemas) =>
            schemas.ToDictionary(x => x.Key, x => x.Value.AsKustoSchema());

        public static Dictionary<string, IKustoSchema> AsKustoSchema(this Dictionary<string, FunctionSchema> schemas) =>
            schemas.ToDictionary(x => x.Key, x => x.Value.AsKustoSchema());

        public static SchemaDifference AsSchemaDifference(this IKustoSchema schema, Difference difference)
        {
            switch (schema)
            {
                case KustoTableSchema _:
                    return new TableSchemaDifference(difference, schema);
                case KustoFunctionSchema _:
                    return new FunctionSchemaDifference(difference, schema);
                default:
                    throw new InvalidOperationException("Unknown type supplied.");
            }
        }
    }
}