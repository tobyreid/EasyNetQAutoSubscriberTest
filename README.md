EasyNetQAutoSubscriberTest
==========================
connectstring to rabbit is host=localhost in App.config
this app will attempt to publish and consume a test object forever
if you issue a 'net stop rabbitmq', 'net start rabbitmq' the 'EasnetQ consumer dispatch thread' will die, but the publisher will continue filling up the queue - careful!