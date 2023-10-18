using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Attachments.Components;
using Nebuli.API.Features.Enum;
using System.Linq;

namespace Nebuli.API.Features.Structs;

/// <summary>
/// Represents an identifier for firearm attachments.
/// </summary>
public readonly struct AttachmentIdentity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AttachmentIdentity"/> struct.
    /// </summary>
    /// <param name="code">The code of the attachment.</param>
    /// <param name="name">The name of the attachment.</param>
    /// <param name="slot">The slot of the attachment.</param>
    internal AttachmentIdentity(uint code, AttachmentName name, AttachmentSlot slot)
    {
        Code = code;
        Name = name;
        Slot = slot;
    }

    /// <summary>
    /// Gets the attachment code.
    /// </summary>
    public uint Code { get; }

    /// <summary>
    /// Gets the attachment name.
    /// </summary>
    public AttachmentName Name { get; }

    /// <summary>
    /// Gets the attachment slot.
    /// </summary>
    public AttachmentSlot Slot { get; }

    /// <summary>
    /// Gets a <see cref="AttachmentIdentity"/> by its name.
    /// </summary>
    /// <param name="type">The <see cref="FirearmType"/> the attachment belongs to.</param>
    /// <param name="name">The <see cref="AttachmentName"/> of the attachment.</param>
    /// <returns></returns>
    public static AttachmentIdentity Get(FirearmType type, AttachmentName name)
        => Items.Firearm.AvailableAttachments[type]
        .FirstOrDefault(identifier => identifier.Name == name);

    /// <summary>
    /// Compares two <see cref="AttachmentIdentity"/> objects with an <see cref="Attachment"/> object for equality.
    /// </summary>
    /// <param name="left">The <see cref="AttachmentIdentity"/> to compare.</param>
    /// <param name="right">The <see cref="Attachment"/> to compare.</param>
    /// <returns><see langword="true"/> if the values are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(AttachmentIdentity left, Attachment right)
    {
        return (left.Name == right.Name) && (left.Slot == right.Slot);
    }

    public static bool operator ==(Attachment left, AttachmentIdentity right)
    {
        return right == left;
    }

    public static bool operator !=(Attachment left, AttachmentIdentity right)
    {
        return right != left;
    }

    public static uint operator +(uint left, AttachmentIdentity right)
    {
        return right.Code + left;
    }

    /// <summary>
    /// Compares two operands: <see cref="AttachmentIdentity"/> and <see cref="AttachmentIdentity"/>.
    /// </summary>
    /// <param name="left">The left-hand <see cref="AttachmentIdentity"/> operand to compare.</param>
    /// <param name="right">The right-hand <see cref="AttachmentIdentity"/> operand to compare.</param>
    /// <returns><see langword="true"/> if the values are equal.</returns>
    public static bool operator ==(AttachmentIdentity left, AttachmentIdentity right) =>
        (left.Name == right.Name) && (left.Code == right.Code) && (left.Slot == right.Slot);

    /// <summary>
    /// Compares two operands: <see cref="AttachmentIdentity"/> and <see cref="AttachmentIdentity"/>.
    /// </summary>
    /// <param name="left">The left-hand <see cref="AttachmentIdentity"/> operand to compare.</param>
    /// <param name="right">The right-hand <see cref="AttachmentIdentity"/> operand to compare.</param>
    /// <returns><see langword="true"/> if the values are not equal.</returns>
    public static bool operator !=(AttachmentIdentity left, AttachmentIdentity right) =>
        (left.Name != right.Name) || (left.Code != right.Code) || (left.Slot != right.Slot);

    /// <summary>
    /// Compares two <see cref="AttachmentIdentity"/> objects with an <see cref="Attachment"/> object for inequality.
    /// </summary>
    /// <param name="left">The <see cref="AttachmentIdentity"/> to compare.</param>
    /// <param name="right">The <see cref="Attachment"/> to compare.</param>
    /// <returns><see langword="true"/> if the values are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(AttachmentIdentity left, Attachment right)
    {
        return left.Name != right.Name || left.Slot != right.Slot;
    }

    public override bool Equals(object obj)
    {
        if (obj is Attachment attachment)
        {
            return this == attachment;
        }
        return false;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Name.ToString();
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public bool Equals(Attachment firearmAttachment)
    {
        return this == firearmAttachment;
    }

    /// <summary>
    /// Indicates whether this instance and an <see cref="AttachmentIdentity"/> are equal.
    /// </summary>
    /// <param name="attachmentIdentity">The <see cref="AttachmentIdentity"/> to compare with the current instance.</param>
    /// <returns><see langword="true"/> if <see cref="AttachmentIdentity"/> and this instance represent the same value; otherwise, <see langword="false"/>.</returns>
    public bool Equals(AttachmentIdentity attachmentIdentity)
    {
        return this == attachmentIdentity;
    }
}