﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IO.Ably.CustomSerialisers {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("MsgPack.Serialization.CodeDomSerializers.CodeDomSerializerBuilder", "0.6.0.0")]
    public class IO_Ably_InboundMessageTrafficSerializer : MsgPack.Serialization.MessagePackSerializer<IO.Ably.InboundMessageTraffic> {
        
        private MsgPack.Serialization.MessagePackSerializer<string> _serializer0;
        
        private MsgPack.Serialization.MessagePackSerializer<IO.Ably.MessageTypes> _serializer1;
        
        public IO_Ably_InboundMessageTrafficSerializer(MsgPack.Serialization.SerializationContext context) : 
                base(context) {
            MsgPack.Serialization.PolymorphismSchema schema0 = default(MsgPack.Serialization.PolymorphismSchema);
            schema0 = null;
            this._serializer0 = context.GetSerializer<string>(schema0);
            MsgPack.Serialization.PolymorphismSchema schema1 = default(MsgPack.Serialization.PolymorphismSchema);
            schema1 = null;
            this._serializer1 = context.GetSerializer<IO.Ably.MessageTypes>(schema1);
        }
        
        protected override void PackToCore(MsgPack.Packer packer, IO.Ably.InboundMessageTraffic objectTree) {
            packer.PackMapHeader(3);
            this._serializer0.PackTo(packer, "all");
            this._serializer1.PackTo(packer, objectTree.All);
            this._serializer0.PackTo(packer, "realtime");
            this._serializer1.PackTo(packer, objectTree.Realtime);
            this._serializer0.PackTo(packer, "rest");
            this._serializer1.PackTo(packer, objectTree.Rest);
        }
        
        protected override IO.Ably.InboundMessageTraffic UnpackFromCore(MsgPack.Unpacker unpacker)
        {
            IO.Ably.InboundMessageTraffic result = default(IO.Ably.InboundMessageTraffic);
            result = new IO.Ably.InboundMessageTraffic();
            int itemsCount0 = default(int);
            itemsCount0 = MsgPack.Serialization.UnpackHelpers.GetItemsCount(unpacker);
            for (int i = 0; (i < itemsCount0); i = (i + 1))
            {
                string key = default(string);
                string nullable4 = default(string);
                nullable4 = MsgPack.Serialization.UnpackHelpers.UnpackStringValue(unpacker,
                    typeof(IO.Ably.InboundMessageTraffic), "MemberName");
                if (((nullable4 == null)
                     == false))
                {
                    key = nullable4;
                }
                else
                {
                    throw MsgPack.Serialization.SerializationExceptions.NewNullIsProhibited("MemberName");
                }
                if ((key == "rest"))
                {
                    IO.Ably.MessageTypes nullable9 = default(IO.Ably.MessageTypes);
                    if ((unpacker.Read() == false))
                    {
                        throw MsgPack.Serialization.SerializationExceptions.NewMissingItem(i);
                    }
                    if (((unpacker.IsArrayHeader == false)
                         && (unpacker.IsMapHeader == false)))
                    {
                        nullable9 = this._serializer1.UnpackFrom(unpacker);
                    }
                    else
                    {
                        MsgPack.Unpacker disposable8 = default(MsgPack.Unpacker);
                        disposable8 = unpacker.ReadSubtree();
                        try
                        {
                            nullable9 = this._serializer1.UnpackFrom(disposable8);
                        }
                        finally
                        {
                            if (((disposable8 == null)
                                 == false))
                            {
                                disposable8.Dispose();
                            }
                        }
                    }
                    if (((nullable9 == null)
                         == false))
                    {
                        result.Rest = nullable9;
                    }
                }
                else
                {
                    if ((key == "realtime"))
                    {
                        IO.Ably.MessageTypes nullable8 = default(IO.Ably.MessageTypes);
                        if ((unpacker.Read() == false))
                        {
                            throw MsgPack.Serialization.SerializationExceptions.NewMissingItem(i);
                        }
                        if (((unpacker.IsArrayHeader == false)
                             && (unpacker.IsMapHeader == false)))
                        {
                            nullable8 = this._serializer1.UnpackFrom(unpacker);
                        }
                        else
                        {
                            MsgPack.Unpacker disposable7 = default(MsgPack.Unpacker);
                            disposable7 = unpacker.ReadSubtree();
                            try
                            {
                                nullable8 = this._serializer1.UnpackFrom(disposable7);
                            }
                            finally
                            {
                                if (((disposable7 == null)
                                     == false))
                                {
                                    disposable7.Dispose();
                                }
                            }
                        }
                        if (((nullable8 == null)
                             == false))
                        {
                            result.Realtime = nullable8;
                        }
                    }
                    else
                    {
                        if ((key == "all"))
                        {
                            IO.Ably.MessageTypes nullable5 = default(IO.Ably.MessageTypes);
                            if ((unpacker.Read() == false))
                            {
                                throw MsgPack.Serialization.SerializationExceptions.NewMissingItem(i);
                            }
                            if (((unpacker.IsArrayHeader == false)
                                 && (unpacker.IsMapHeader == false)))
                            {
                                nullable5 = this._serializer1.UnpackFrom(unpacker);
                            }
                            else
                            {
                                MsgPack.Unpacker disposable4 = default(MsgPack.Unpacker);
                                disposable4 = unpacker.ReadSubtree();
                                try
                                {
                                    nullable5 = this._serializer1.UnpackFrom(disposable4);
                                }
                                finally
                                {
                                    if (((disposable4 == null)
                                         == false))
                                    {
                                        disposable4.Dispose();
                                    }
                                }
                            }
                            if (((nullable5 == null)
                                 == false))
                            {
                                result.All = nullable5;
                            }
                        }
                        else
                        {
                            unpacker.Skip();
                        }
                    }
                }
            }
            return result;
        }

        private static T @__Conditional<T>(bool condition, T whenTrue, T whenFalse)
         {
            if (condition) {
                return whenTrue;
            }
            else {
                return whenFalse;
            }
        }
    }
}
