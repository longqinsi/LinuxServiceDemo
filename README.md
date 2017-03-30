# LinuxServiceDemo
Use Supervisord, TopShelf.Linux, [anyexec](https://linuxdot.net/down/anyexec-1.2-linux_x64.tar.gz) and Mono.Unix to gracefully stop monoservice on Linux

The config of supervisord is below (given the username is someuser):

[program:LinuxServiceDemo]<br />
directory=/home/someuser/demo/app<br />
command=/home/someuser/demo/LinuxServiceDemo<br />
user=someuser<br />
process_name=%(program_name)s_%(process_num)02d<br />
priority=1<br />
numprocs=1<br />
autostart=true<br />
autorestart=false<br />
startretries=10<br />
exitcodes=0<br />
stopsignal=INT<br />
stopwaitsecs=10<br />
redirect_stderr=true<br />
stdout_logfile=/home/someuser/log/LinuxServiceDemo.log
