# LinuxServiceDemo
Use Supervisord, TopShelf.Linux and Mono.Unix to gracefully stop monoservice on Linux

The config of supervisord is below (given the username is someuser):

[program:LinuxServiceDemo]
directory=/home/someuser/demo/app
;command=mono-service /home/someuser/LinuxServiceDemo/LinuxServiceDemo.exe --no-daemon
command=/home/someuser/demo/LinuxServiceDemo
user=someuser
process_name=%(program_name)s_%(process_num)02d
priority=1
numprocs=1
autostart=true
autorestart=false
startretries=10
exitcodes=0
stopsignal=INT
stopwaitsecs=10
redirect_stderr=true
stdout_logfile=/home/someuser/log/LinuxServiceDemo.log
