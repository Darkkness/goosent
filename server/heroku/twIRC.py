import requests
import socket

token = 'oauth:aew3ogk12mdl0lob3gt08cph4mhro1'
client_id = 'xb82yspb0vysx265h5oabocd6ytq6w'
nick = 'goosentproject'
channel = ['miwarapoport', 'korob22']
#i = 'miwarapoport'

server = 'irc.chat.twitch.tv'
sport = 6667

msg = "hi"

s = socket.socket()
s.connect((server, sport))
s.send('PASS {}\r\n'.format(token).encode('utf-8'))
s.send('NICK {}\r\n'.format(nick).encode('utf-8'))
for i in channel:
    print(i)
    s.send('JOIN #{}\r\n'.format(i).encode('utf-8'))
#s.send('PRIVMSG #{} :{}\r\n'.format(channel, msg).encode('utf-8'))



while True:
    response = s.recv(1024).decode('utf-8')

    if response == 'PING :tmi.twitch.tv':
            s.send('PONG :tmi.twitch.tv'.encode('utf-8'))

    else:
        if response != None and response != '':
            print(response)
