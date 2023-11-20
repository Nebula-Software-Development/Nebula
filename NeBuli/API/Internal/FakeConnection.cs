// -----------------------------------------------------------------------
// <copyright file=FakeConnection.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Mirror;
using System;

namespace Nebuli.API.Internal;

internal class FakeConnection : NetworkConnectionToClient
{
    public override void Send(ArraySegment<byte> segment, int channelId = 0)
    {
    }

    public override string address => "localhost";

    public FakeConnection(int networkConnectionId) : base(networkConnectionId)
    {
    }
}