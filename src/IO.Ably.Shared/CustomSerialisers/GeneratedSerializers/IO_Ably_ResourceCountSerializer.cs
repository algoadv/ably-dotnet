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
    public class IO_Ably_ResourceCountSerializer : MsgPack.Serialization.MessagePackSerializer<IO.Ably.ResourceCount> {
        
        private MsgPack.Serialization.MessagePackSerializer<string> _serializer0;
        
        private MsgPack.Serialization.MessagePackSerializer<double> _serializer1;
        
        public IO_Ably_ResourceCountSerializer(MsgPack.Serialization.SerializationContext context) : 
                base(context) {
            MsgPack.Serialization.PolymorphismSchema schema0 = default(MsgPack.Serialization.PolymorphismSchema);
            schema0 = null;
            this._serializer0 = context.GetSerializer<string>(schema0);
            MsgPack.Serialization.PolymorphismSchema schema1 = default(MsgPack.Serialization.PolymorphismSchema);
            schema1 = null;
            this._serializer1 = context.GetSerializer<double>(schema1);
        }
        
        protected override void PackToCore(MsgPack.Packer packer, IO.Ably.ResourceCount objectTree) {
            packer.PackMapHeader(5);
            this._serializer0.PackTo(packer, "mean");
            this._serializer1.PackTo(packer, objectTree.Mean);
            this._serializer0.PackTo(packer, "min");
            this._serializer1.PackTo(packer, objectTree.Min);
            this._serializer0.PackTo(packer, "opened");
            this._serializer1.PackTo(packer, objectTree.Opened);
            this._serializer0.PackTo(packer, "peak");
            this._serializer1.PackTo(packer, objectTree.Peak);
            this._serializer0.PackTo(packer, "refused");
            this._serializer1.PackTo(packer, objectTree.Refused);
        }
        
        protected override IO.Ably.ResourceCount UnpackFromCore(MsgPack.Unpacker unpacker)
        {
            IO.Ably.ResourceCount result = default(IO.Ably.ResourceCount);
            result = new IO.Ably.ResourceCount();
            int itemsCount0 = default(int);
            itemsCount0 = MsgPack.Serialization.UnpackHelpers.GetItemsCount(unpacker);
            for (int i = 0; (i < itemsCount0); i = (i + 1))
            {
                string key = default(string);
                string nullable4 = default(string);
                nullable4 = MsgPack.Serialization.UnpackHelpers.UnpackStringValue(unpacker,
                    typeof(IO.Ably.ResourceCount), "MemberName");
                if (((nullable4 == null)
                     == false))
                {
                    key = nullable4;
                }
                else
                {
                    throw MsgPack.Serialization.SerializationExceptions.NewNullIsProhibited("MemberName");
                }
                if ((key == "refused"))
                {
                    System.Nullable<double> nullable9 = default(System.Nullable<double>);
                    nullable9 = MsgPack.Serialization.UnpackHelpers.UnpackNullableDoubleValue(unpacker,
                        typeof(IO.Ably.ResourceCount), "Double Refused");
                    if (nullable9.HasValue)
                    {
                        result.Refused = nullable9.Value;
                    }
                }
                else
                {
                    if ((key == "peak"))
                    {
                        System.Nullable<double> nullable8 = default(System.Nullable<double>);
                        nullable8 = MsgPack.Serialization.UnpackHelpers.UnpackNullableDoubleValue(unpacker,
                            typeof(IO.Ably.ResourceCount), "Double Peak");
                        if (nullable8.HasValue)
                        {
                            result.Peak = nullable8.Value;
                        }
                    }
                    else
                    {
                        if ((key == "opened"))
                        {
                            System.Nullable<double> nullable7 = default(System.Nullable<double>);
                            nullable7 = MsgPack.Serialization.UnpackHelpers.UnpackNullableDoubleValue(unpacker,
                                typeof(IO.Ably.ResourceCount), "Double Opened");
                            if (nullable7.HasValue)
                            {
                                result.Opened = nullable7.Value;
                            }
                        }
                        else
                        {
                            if ((key == "min"))
                            {
                                System.Nullable<double> nullable6 = default(System.Nullable<double>);
                                nullable6 = MsgPack.Serialization.UnpackHelpers.UnpackNullableDoubleValue(unpacker,
                                    typeof(IO.Ably.ResourceCount), "Double Min");
                                if (nullable6.HasValue)
                                {
                                    result.Min = nullable6.Value;
                                }
                            }
                            else
                            {
                                if ((key == "mean"))
                                {
                                    System.Nullable<double> nullable5 = default(System.Nullable<double>);
                                    nullable5 = MsgPack.Serialization.UnpackHelpers.UnpackNullableDoubleValue(unpacker,
                                        typeof(IO.Ably.ResourceCount), "Double Mean");
                                    if (nullable5.HasValue)
                                    {
                                        result.Mean = nullable5.Value;
                                    }
                                }
                                else
                                {
                                    unpacker.Skip();
                                }
                            }
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
