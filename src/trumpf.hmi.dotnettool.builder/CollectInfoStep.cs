using System;

namespace trumpf.hmi.dotnettool.builder
{
    public abstract class CollectInfoStep : ICollectInfo
    {
        protected CollectInfoStep(string title)
        {
            Title = title;
        }

        public string Title { get; }

        public virtual string Invoke()
        {
            Console.WriteLine(Title);
            return Console.ReadLine();
        }
    }
}