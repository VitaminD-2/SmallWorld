﻿using Fusion;

namespace Assets.Model
{
    public struct Race : INetworkStruct
    {
        public NetworkString<_16> Name { get; set; }
        public int Tokens { get; set; }
        public bool InDecline { get; set; }
    }
}
