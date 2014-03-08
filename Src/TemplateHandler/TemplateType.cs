/******************************************************************
 *
 * ����ģ�飺Enums(ϵͳö��ģ��)
 * �� �� �ƣ�TemplateType(ģ������)
 * ����������ϵͳģ�������
 * 
 * ------------������Ϣ------------------
 * ��    �ߣ�Nick
 * ��    �ڣ�2009-08-06
 * ajj82@163.com
 * MSN:ajj82@163.com
 * QQ:46810878
 * ------------�༭�޸���Ϣ--------------
 * ��    �ߣ�Fernando Hua
 * ��    �ڣ�2010-04-09
 * ��    �ݣ�����ͶƱģ������
******************************************************************/
using System;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// ϵͳģ������
    /// </summary>
    public enum TemplateType
    {
        /// <summary>
        /// δ֪
        /// </summary>
        Unknow = 0,

        /// <summary>
        /// ��ҳģ��
        /// </summary>
        Tem_Index = 1,

        /// <summary>
        /// ��վ���浯��ҳģ��
        /// </summary>
        Tem_Announce = 2,

        ///// <summary>
        ///// ��վ��������ҳģ��
        ///// </summary>
        //Tem_AnnounceItem = 3,

        
        ///// <summary>
        ///// ��վ�����б�ҳģ��
        ///// </summary>
        //Tem_AnnounceList = 4,


        ///// <summary>
        ///// ������ڵ���ҳģ��
        ///// </summary>
        //Tem_NodeIndex = 5,

        /// <summary>
        /// ������ڵ���Ŀҳģ�壨��������Ŀ�������
        /// </summary>
        //Tem_NodeCategoryChild = 7,

        /// <summary>
        /// ������ڵ���Ŀҳģ�壨��������Ŀ��
        /// </summary>
        Tem_Node = 8,

        /// <summary>
        /// ������ڵ���Ŀҳģ�壨����������Ŀ�������
        /// </summary>
        //Tem_NodeCategory = 9,


        /// <summary>
        /// �ڵ�����ҳģ��
        /// </summary>
        Tem_NodeContent = 11,

        /// <summary>
        /// �������ҳ
        /// </summary>
        Tem_SearchResult = 13,

        /// <summary>
        /// �ʾ������ҳģ��
        /// </summary>
        Tem_SurveyResult = 14,

        /// <summary>
        /// ͶƱ���ҳģ��
        /// </summary>
        Tem_VoteResult = 15,

        /// <summary>
        /// ר��ҳģ��
        /// </summary>
        Tem_Special = 16,

        ///// <summary>
        ///// ���԰���ҳģ��
        ///// </summary>
        //Tem_GuestBook_Index = 20,


        /// <summary>
        /// ��������ģ��
        /// </summary>
        Tem_GuestBook_Add = 22,


        ///// <summary>
        ///// ���Իظ�ģ��
        ///// </summary>
        //Tem_GuestBook_Reply = 24,


        /// <summary>
        /// ��������ҳģ��
        /// </summary>
        //Tem_GuestBook_SearchResult = 26,

        /// <summary>
        /// ��Աע���ģ��
        /// </summary>
        Tem_User_RegForm = 31,

        /// <summary>
        /// ��Աע����ģ��
        /// </summary>
        Tem_User_RegResult = 33,

        /// <summary>
        /// ��Ա��¼ģ��
        /// </summary>
        Tem_User_Login = 35,

        /// <summary>
        /// ��������ҳģ��
        /// </summary>
        Tem_Comment = 40,

        ///// <summary>
        ///// �Զ�����Ŀģ��
        ///// </summary>
        //Tem_Custom = 999,

        ///// <summary>
        ///// �Զ�������ģ��
        ///// </summary>
        //Tem_CustomContent = 990,

        /// <summary>
        /// �Զ����ģ��
        /// </summary>
        Tem_CustomForm = 99,
    }

    /// <summary>
    /// ϵͳģ������
    /// </summary>
    public enum TemplateTypeCN
    {
        /// <summary>
        /// δ֪
        /// </summary>
        δ֪ = 0,

        /// <summary>
        /// ��ҳģ��
        /// </summary>
        ��ҳģ�� = 1,

        /// <summary>
        /// ��վ���浯��ҳģ��
        /// </summary>
        ��վ���浯��ҳģ�� = 2,

        ///// <summary>
        ///// ��վ����ģ��
        ///// </summary>
        //��վ��������ҳģ�� = 3,

        ///// <summary>
        ///// ��վ�����б�ҳģ��
        ///// </summary>
        //��վ�����б�ҳģ�� = 4,

        ///// <summary>
        ///// ������ڵ���ҳģ��
        ///// </summary>
        //������ڵ���ҳģ�� = 5,

        /// <summary>
        /// ������ڵ���Ŀҳģ�壨��������Ŀ�������
        /// </summary>
        //������ڵ���Ŀҳģ��_��������Ŀ = 7,
        
        /// <summary>
        /// ������ڵ���Ŀҳģ��
        /// </summary>
        ����ڵ���Ŀҳģ�� = 8,

        /// <summary>
        /// ������ڵ���Ŀҳģ�壨����������Ŀ�������
        /// </summary>
        //������ڵ���Ŀҳģ��_����������Ŀ = 9,


        /// <summary>
        /// �ڵ�����ҳģ��
        /// </summary>
        �ڵ�����ҳģ�� = 11,

        /// <summary>
        /// ����ҳ
        /// </summary>
        �������ҳģ�� = 13,

        /// <summary>
        /// �ʾ������ҳģ��
        /// </summary>
        �ʾ������ҳģ�� = 14,

        /// <summary>
        /// ͶƱ���ҳģ��
        /// </summary>
        ͶƱ���ҳģ�� = 15,

        /// <summary>
        /// ר��ҳģ��
        /// </summary>
        ר��ҳģ�� = 16,

        ///// <summary>
        ///// ���԰���ҳģ��
        ///// </summary>
        //���԰���ҳģ�� = 20,


        /// <summary>
        /// ��������ģ��
        /// </summary>
        ��������ģ�� = 22,


        ///// <summary>
        ///// ���Իظ�ģ��
        ///// </summary>
        //���Իظ�ģ�� = 24,


        /// <summary>
        /// ��������ҳģ��
        /// </summary>
        //��������ҳģ�� = 26,

        /// <summary>
        /// ��Աע���ģ��
        /// </summary>
        ��Աע���ģ�� = 31,

        /// <summary>
        /// ��Աע����ģ��
        /// </summary>
        ��Աע����ģ�� = 33,

        /// <summary>
        /// ��Ա��¼ģ��
        /// </summary>
        ��Ա��¼ģ�� = 35,

        /// <summary>
        /// ��������ҳģ��
        /// </summary>
        ���ۻظ�ҳģ�� = 40,

        ///// <summary>
        ///// �Զ�����Ŀҳģ��
        ///// </summary>
        //�Զ�����Ŀҳģ�� = 999,

        ///// <summary>
        ///// �Զ�������ҳģ��
        ///// </summary>
        //�Զ�������ҳģ�� = 990,

        /// <summary>
        /// �Զ����ģ��
        /// </summary>
        �Զ����ģ�� = 99,
    }
}
