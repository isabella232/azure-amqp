// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Amqp.Framing
{
    using System.Text;

    /// <summary>
    /// Defines the received outcome.
    /// </summary>
    public sealed class Received : DeliveryState
    {
        /// <summary>Descriptor name.</summary>
        public static readonly string Name = "amqp:received:list";
        /// <summary>Descriptor code.</summary>
        public static readonly ulong Code = 0x0000000000000023;
        const int Fields = 2;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public Received() : base(Name, Code) { }

        /// <summary>
        /// Gets or sets the "section-number" field.
        /// </summary>
        public uint? SectionNumber { get; set; }

        /// <summary>
        /// Gets or sets the "section-offset" field.
        /// </summary>
        public ulong? SectionOffset { get; set; }

        /// <summary>
        /// Gets the number of fields in the list.
        /// </summary>
        protected override int FieldCount
        {
            get { return Fields; }
        }

        /// <summary>
        /// Returns a string that represents the object.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("received(");
            int count = 0;
            this.AddFieldToString(this.SectionNumber != null, sb, "section-number", this.SectionNumber, ref count);
            this.AddFieldToString(this.SectionOffset != null, sb, "section-offset", this.SectionOffset, ref count);
            sb.Append(')');
            return sb.ToString();
        }

        /// <summary>
        /// Encodes the fields into the buffer.
        /// </summary>
        /// <param name="buffer">The buffer to write.</param>
        protected override void OnEncode(ByteBuffer buffer)
        {
            AmqpCodec.EncodeUInt(this.SectionNumber, buffer);
            AmqpCodec.EncodeULong(this.SectionOffset, buffer);
        }

        /// <summary>
        /// Decodes the fields from the buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="count">The number of fields.</param>
        protected override void OnDecode(ByteBuffer buffer, int count)
        {
            if (count-- > 0)
            {
                this.SectionNumber = AmqpCodec.DecodeUInt(buffer);
            }

            if (count-- > 0)
            {
                this.SectionOffset = AmqpCodec.DecodeULong(buffer);
            }
        }

        /// <summary>
        /// Returns the total encode size of all fields.
        /// </summary>
        /// <returns>The total encode size.</returns>
        protected override int OnValueSize()
        {
            int valueSize = 0;

            valueSize += AmqpCodec.GetUIntEncodeSize(this.SectionNumber);
            valueSize += AmqpCodec.GetULongEncodeSize(this.SectionOffset);

            return valueSize;
        }
    }
}
