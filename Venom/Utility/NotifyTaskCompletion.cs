using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Utility
{
    public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        public Task<TResult> Task { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public TResult Result => ( Task.Status == TaskStatus.RanToCompletion ) ? Task.Result : default;

        public TaskStatus Status => Task.Status;
        public bool IsCompleted => Task.IsCompleted;
        public bool IsNotCompleted => !Task.IsCompleted;

        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;
        public bool IsCanceled => Task.IsCanceled;
        public bool IsFaulted => Task.IsFaulted;

        public AggregateException Exception => Task.Exception;
        public Exception InnerException => Exception?.InnerException;
        public string ErrorMessage => InnerException?.Message;




        public NotifyTaskCompletion( Task<TResult> task )
        {
            Task = task;

            if( !task.IsCompleted )
            {
                var _ = WatchTaskAsync( task );
            }
        }


        private async Task WatchTaskAsync( Task task )
        {
            try
            {
                await task
                    .ConfigureAwait( true );
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
            {
	            ;
            }
#pragma warning restore CA1031 // Do not catch general exception types

            var propertyChanged = PropertyChanged;
            if( propertyChanged == null )
            {
                return;
            }

            propertyChanged( this, new PropertyChangedEventArgs( nameof( Status ) ) );
            propertyChanged( this, new PropertyChangedEventArgs( nameof( IsCompleted ) ) );
            propertyChanged( this, new PropertyChangedEventArgs( nameof( IsNotCompleted ) ) );

            if( task.IsCanceled )
            {
                propertyChanged( this, new PropertyChangedEventArgs( nameof( IsCanceled ) ) );
            }
            else if( task.IsFaulted )
            {
                propertyChanged( this, new PropertyChangedEventArgs( nameof( IsFaulted ) ) );
                propertyChanged( this, new PropertyChangedEventArgs( nameof( Exception ) ) );
                propertyChanged( this, new PropertyChangedEventArgs( nameof( InnerException ) ) );
                propertyChanged( this, new PropertyChangedEventArgs( nameof( ErrorMessage ) ) );
            }
            else
            {
                propertyChanged( this, new PropertyChangedEventArgs( nameof( IsSuccessfullyCompleted ) ) );
                propertyChanged( this, new PropertyChangedEventArgs( nameof( Result ) ) );
            }
        }
    }
}
