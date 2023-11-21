// -----------------------------------------------------------------------
// <copyright file=CustomVectorsConverter.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Nebuli.Loader.CustomConverters
{
    public sealed class CustomVectorsConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type) => type == typeof(Vector2) || type == typeof(Vector3) || type == typeof(Vector4);

        public object ReadYaml(IParser parser, Type type)
        {
            if (!parser.TryConsume<MappingStart>(out _))
            {
                throw new InvalidDataException($"Cannot deserialize object of type {type.FullName}.");
            }

            List<float> coordinates = new();

            int i = 0;
            while (!parser.TryConsume<MappingEnd>(out _))
            {
                if (i++ % 2 == 0)
                {
                    parser.MoveNext();
                    continue;
                }

                if (!parser.TryConsume(out Scalar scalar) || !float.TryParse(scalar.Value, NumberStyles.Float,
                        CultureInfo.GetCultureInfo("en-US"), out float coordinate))
                {
                    throw new InvalidDataException("Invalid float value.");
                }

                coordinates.Add(coordinate);
            }

            object vector = type switch
            {
                not null when type == typeof(Vector2) && coordinates.Count == 2 => new Vector2(coordinates[0],
                    coordinates[1]),
                not null when type == typeof(Vector3) && coordinates.Count == 3 => new Vector3(coordinates[0],
                    coordinates[1], coordinates[2]),
                not null when type == typeof(Vector4) && coordinates.Count == 4 => new Vector4(coordinates[0],
                    coordinates[1], coordinates[2], coordinates[3]),
                _ => throw new InvalidDataException($"Invalid type or number of coordinates for {type.FullName}.")
            };
            return vector;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            Dictionary<string, float> coordinates = new();

            switch (value)
            {
                case Vector2 vector2:
                    coordinates["x"] = vector2.x;
                    coordinates["y"] = vector2.y;
                    break;

                case Vector3 vector3:
                    coordinates["x"] = vector3.x;
                    coordinates["y"] = vector3.y;
                    coordinates["z"] = vector3.z;
                    break;

                case Vector4 vector4:
                    coordinates["x"] = vector4.x;
                    coordinates["y"] = vector4.y;
                    coordinates["z"] = vector4.z;
                    coordinates["w"] = vector4.w;
                    break;

                default:
                    throw new InvalidDataException($"Invalid type for {type.FullName}.");
            }

            emitter.Emit(new MappingStart());

            foreach (KeyValuePair<string, float> coordinate in coordinates)
            {
                emitter.Emit(new Scalar(coordinate.Key));
                emitter.Emit(new Scalar(coordinate.Value.ToString(CultureInfo.GetCultureInfo("en-US"))));
            }

            emitter.Emit(new MappingEnd());
        }
    }
}