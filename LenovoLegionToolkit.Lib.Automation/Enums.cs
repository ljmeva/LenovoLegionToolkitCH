using System.ComponentModel.DataAnnotations;

namespace LenovoLegionToolkit.Lib.Automation
{
    public enum DeactivateGPUAutomationStepState
    {
        [Display(Name = "结束应用")]
        KillApps,
        [Display(Name = "重启GPU")]
        RestartGPU,
    }
}
