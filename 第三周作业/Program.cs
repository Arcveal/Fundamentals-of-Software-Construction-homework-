using System;
using System.Threading;

// 闹钟类
public class Clock
{
    // 定义事件参数类，传递当前时间
    public class ClockEventArgs : EventArgs
    {
        public DateTime CurrentTime { get; set; }
    }

    // 滴答事件：每秒触发
    public event EventHandler<ClockEventArgs> Tick;
    // 响铃事件：到达闹钟时间触发
    public event EventHandler<ClockEventArgs> Alarm;

    public DateTime AlarmTime { get; set; }
    private DateTime currentTime;

    public Clock(DateTime alarmTime)
    {
        AlarmTime = alarmTime;
        currentTime = DateTime.Now;
    }

    // 启动时钟
    public void Start()
    {
        while (true)
        {
            currentTime = DateTime.Now;
            // 触发Tick事件
            OnTick(new ClockEventArgs { CurrentTime = currentTime });

            // 检查是否到达闹钟时间（精确到秒）
            if (currentTime.Hour == AlarmTime.Hour &&
                currentTime.Minute == AlarmTime.Minute &&
                currentTime.Second == AlarmTime.Second)
            {
                OnAlarm(new ClockEventArgs { CurrentTime = currentTime });
                break; // 响铃后停止
            }

            Thread.Sleep(1000); // 暂停1秒
        }
    }

    // 触发Tick事件的保护方法
    protected virtual void OnTick(ClockEventArgs e)
    {
        Tick?.Invoke(this, e);
    }

    // 触发Alarm事件的保护方法
    protected virtual void OnAlarm(ClockEventArgs e)
    {
        Alarm?.Invoke(this, e);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("请输入闹钟时间（格式：HH:mm:ss）：");
        string alarmTimeStr = Console.ReadLine();
        DateTime alarmTime = DateTime.ParseExact(alarmTimeStr, "HH:mm:ss", null);
        alarmTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, alarmTime.Hour, alarmTime.Minute, alarmTime.Second);

        Clock clock = new Clock(alarmTime);

        // 订阅Tick事件
        clock.Tick += (sender, e) =>
        {
            Console.WriteLine($"[{e.CurrentTime:HH:mm:ss}] 滴答...");
        };

        // 订阅Alarm事件
        clock.Alarm += (sender, e) =>
        {
            Console.WriteLine($"🔔 {e.CurrentTime:HH:mm:ss} 闹钟响了！！！");
        };

        Console.WriteLine("闹钟已启动...");
        clock.Start();
    }
}