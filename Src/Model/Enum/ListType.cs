using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hope.ITMS.Enums
{
    /// <summary>
    /// 自定义字段列表类型
    /// </summary>
    public enum ListType
    {
        None = 0,

        /// <summary>
        /// 实验室检查项目
        /// </summary>
        LabExamine = 1,

        /// <summary>
        /// 治疗反应
        /// </summary>
        TreatResponse = 2,

        /// <summary>
        /// 诱导缓解_SR患者用
        /// </summary>
        Derivation_SR = 3,

        /// <summary>
        /// 诱导缓解_IR与HR患者用
        /// </summary>
        Derivation_IHR = 4,

        /// <summary>
        /// 急性毒副作用观察
        /// </summary>
        ToxicSideEffect = 5,

        /// <summary>
        /// 巩固治疗
        /// </summary>
        AfterTreatment = 6,

        /// <summary>
        /// 再诱导
        /// </summary>
        ReDerivation = 7,
    }

    /// <summary>
    /// 自定义字段列表类型
    /// </summary>
    public enum ListTypeCN
    {
        未知 = 0,

        /// <summary>
        /// 实验室检查项目
        /// </summary>
        实验室检查 = 1,

        /// <summary>
        /// 治疗反应
        /// </summary>
        治疗反应 = 2,

        /// <summary>
        /// 诱导缓解_SR患者用
        /// </summary>
        SR患者诱导缓解 = 3,

        /// <summary>
        /// 诱导缓解_IR与HR患者用
        /// </summary>
        IR与HR患者诱导缓解 = 4,

        /// <summary>
        /// 急性毒副作用观察
        /// </summary>
        急性毒副作用观察=5,

        /// <summary>
        /// 巩固治疗
        /// </summary>
        巩固治疗 = 6,

        /// <summary>
        /// 再诱导
        /// </summary>
        再诱导 = 7,
    }
}
