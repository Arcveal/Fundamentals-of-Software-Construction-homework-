using System.Collections.Generic;

namespace 第八周作业
{
    public class WordManager
    {
        private List<Word> wordList;

        public WordManager()
        {
            // 直接在内存里初始化单词列表，相当于“数据库”
            wordList = new List<Word>
            {
                new Word("apple", "苹果"),
                new Word("banana", "香蕉"),
                new Word("computer", "电脑"),
                new Word("program", "程序"),
                new Word("student", "学生"),
                new Word("teacher", "老师"),
                new Word("book", "书本"),
                new Word("pen", "钢笔")
            };
        }

        public List<Word> GetAllWords()
        {
            return wordList;
        }
    }
}