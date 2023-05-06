namespace CSharp
{
    public class GENetDefine
    {
        
        enum PackType
        {
            IntFlag = -98,
            LongFlag = -99,
            NoneFlag = -100,
            TrueFlag = -101,
            FalseFlag = -102,
            TableFlag = -103,
            StringFlag = -106
        }


        // 最大递归层数
        public static int MAX_STACK_DEEP = 30;

        // 消息头的长度
        public static int MSG_HEAD_SIZE = 8;
        
        // 默认buf大小
        public static int DEFAULT_BUF_SIZE = 1024;

    }
}