namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems.PowerQuality;

public enum PqSignalSegmentLabel
{
    NoFault,

    PhaseAtoGround,
    PhaseBtoGround,
    PhaseCtoGround,

    PhaseABtoGround,
    PhaseACtoGround,
    PhaseBCtoGround,

    PhaseABCtoGround,

    PhaseAtoB,
    PhaseAtoC,
    PhaseBtoC,
}