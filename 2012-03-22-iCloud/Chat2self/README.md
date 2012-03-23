chat2self
==========

This sample is a totally useless but kinda cute demonstration of the
iOS5 iCloud Key-Value Storage feature.

NOTE: you need at least two devices with the same App Store login to play.

iCloud Key-Value Storage lets your code share a collection of key-value
pairs across all devices registered with the same App Store login. That's
why it is called chat2_self_.

To set it up on your system you'll need to configure the BundleId, Provisioning Profile,
and Entitlements.plist (with your TeamId) so that iCloud will work.

Each time you enter a message, it is saved to the local Key-Value storage
collection. iOS then decides when to synchronize that key with iCloud. 
Other devices similarly decide when to synchronise their local iCloud
key-value storage, and when they detect new values they trigger a notification
that causes the latest value of each key to be displayed in the message list.

![screenshot](http://4.bp.blogspot.com/-SufxNqZ61IE/T04BgKrrqAI/AAAAAAAABWU/junjqplS1EI/s1600/chat2self.png "Sample") 

You - the developer - has NO control over the timing of iCloud synchronisation.
Using this app will give you some idea of the latency between devices updating
their iCloud storage for small values.


Acknowledgements
----------------

This app uses Miguel de Icaza's bubble-chat rendering sample
using MonoTouch.Dialog and a custom ChatBubble element.

https://github.com/xamarin/monotouch-samples/tree/master/BubbleCell



