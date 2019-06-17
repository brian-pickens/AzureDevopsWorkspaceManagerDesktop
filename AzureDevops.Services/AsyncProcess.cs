using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AzureDevops.Services
{
    public class AsyncProcess : IDisposable
    {
        private bool _started;
        private readonly Process _proc;
        private readonly int _timeout;
        private readonly TaskCompletionSource<int> _exitCodeCompletionSource;

        public AsyncProcess(string filename, string arguments, int timeout = -1)
        {
            _timeout = timeout;

            _proc = new Process();

            _proc.StartInfo.FileName = filename;
            _proc.StartInfo.Arguments = arguments;
            _proc.StartInfo.UseShellExecute = false;
            _proc.StartInfo.CreateNoWindow = false;
            _proc.StartInfo.RedirectStandardOutput = true;
            _proc.StartInfo.RedirectStandardError = true;

            _exitCodeCompletionSource = new TaskCompletionSource<int>();
        }

        public bool IsRunning { get; private set; }
        public int ExitCode { get; private set; }

        public AsyncProcess Start()
        {
            if (_started) throw new ApplicationException("Process is already running");

            _proc.Start();
            _proc.EnableRaisingEvents = true;
            _proc.Exited += OnProcOnExited;

            IsRunning = true;
            _started = true;
            return this;
        }

        private void OnProcOnExited(object sender, EventArgs args)
        {
            if (!IsRunning) return;
            var proc = sender as Process;
            IsRunning = false;
            ExitCode = proc.ExitCode;
            _exitCodeCompletionSource.SetResult(proc.ExitCode);
        }

        public async IAsyncEnumerable<string> Output()
        {
            if (!IsRunning)
                throw new ApplicationException("Process is not runninga");

            while (!_proc.StandardOutput.EndOfStream)
            {
                yield return await _proc.StandardOutput.ReadLineAsync();
            }
        }

        public async IAsyncEnumerable<string> Error()
        {
            if (!IsRunning)
                throw new ApplicationException("Process is not runningb");

            while (!_proc.StandardError.EndOfStream)
            {
                yield return await _proc.StandardError.ReadLineAsync();
            }
        }

        public async Task<int> WaitForExitCodeAsync()
        {
            return await _exitCodeCompletionSource.Task;
        }

        public void Dispose()
        {
            _proc?.Dispose();
        }
    }
}
