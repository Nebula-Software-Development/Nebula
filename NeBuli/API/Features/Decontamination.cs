using static LightContainmentZoneDecontamination.DecontaminationController;

namespace Nebuli.API.Features;

public static class Decontamination
{
    private static readonly LightContainmentZoneDecontamination.DecontaminationController Controller = Singleton;

    /// <summary>
    /// Gets if <see cref="LightContainmentZoneDecontamination.DecontaminationController"/> is currently decontaminating.
    /// </summary>
    public static bool IsDecontaminating => Controller.IsDecontaminating;

    /// <summary>
    /// Disables the elevators.
    /// </summary>
    public static void DisableElevators() => Controller.DisableElevators();

    /// <summary>
    /// Finishes the decontamination sequence.
    /// </summary>
    public static void FinishDecontamination() => Controller.FinishDecontamination();

    /// <summary>
    /// Forces Light Containment to be decontaminated.
    /// </summary>
    public static void ForceDecontamination() => Controller.ForceDecontamination();

    /// <summary>
    /// Sets the <see cref="LightContainmentZoneDecontamination.DecontaminationController.DecontaminationStatus"/>.
    /// </summary>
    public static DecontaminationStatus DecontaminationStatus
    {
        get => Controller.DecontaminationOverride;
        set => Controller.NetworkDecontaminationOverride = value;
    }
}