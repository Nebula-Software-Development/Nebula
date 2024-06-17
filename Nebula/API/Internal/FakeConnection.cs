// -----------------------------------------------------------------------
// <copyright file=FakeConnection.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Mirror;

namespace Nebula.API.Internal
{
    internal class FakeConnection : NetworkConnectionToClient
    {
        public FakeConnection(int networkConnectionId) : base(networkConnectionId)
        {
        }

        public override string address => "localhost";

        public override void Send(ArraySegment<byte> segment, int channelId = 0)
        {
        }
    }
}