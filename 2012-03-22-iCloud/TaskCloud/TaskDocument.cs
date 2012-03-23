using System;
using System.IO;
using System.Xml.Serialization;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Cloud {
	[Preserve]
	public class TaskDocument : UIDocument {
	
		[Preserve]	
		public class Task {
			public Task () {}
			public string Title { get; set; }
			public string Description { get; set; }
			public bool IsDone { get; set; }
		}

		public TaskDocument (NSUrl url) : base (url){
			TheTask = new Task() {
				Title = "New Task " + DateTime.Now.ToString("dd MMM yy H:mm:ss")
			};
		}
		
		public Task TheTask { get; set; }
		
		public override string ToString ()
		{
			return string.Format ("[TaskDocument: Title={0}, Description={1}, IsDone={2}]", TheTask.Title, TheTask.Description, TheTask.IsDone);
		}

		NSString DocumentContent 
		{
			get {
				XmlSerializer serializer = new XmlSerializer(typeof(Task));
				System.IO.TextWriter writer = new System.IO.StringWriter();
				try {
					serializer.Serialize(writer, this.TheTask);
				} finally {
					writer.Flush();
					writer.Close();
				}
				return new NSString (writer.ToString());
			}
			set {
				XmlSerializer serializer = new XmlSerializer(typeof(Task));
				System.IO.TextReader reader = new System.IO.StringReader(value.ToString ());
				object @object = default(TaskDocument);    // default return value
				try {
					@object = serializer.Deserialize(reader);
				} finally {
					reader.Close();
				}
				var t =  (Task)@object;
				this.TheTask = t;
			}
		}

		public override bool LoadFromContents (NSObject contents, string typeName, out NSError outError)
		{
			Console.WriteLine ("LoadFromContents("+typeName+")");
			outError = null;

			if (contents != null) {
				this.DocumentContent = NSString.FromData( (NSData)contents, NSStringEncoding.UTF8 );
			}

			NSNotificationCenter.DefaultCenter.PostNotificationName("taskModified", this);

			return true;
		}

		public override NSObject ContentsForType (string typeName, out NSError outError)
		{
			Console.WriteLine ("ContentsForType("+typeName+")");
			
			outError = null;

			Console.WriteLine ("DocumentContent:" + DocumentContent );

			NSData docData = this.DocumentContent.Encode(NSStringEncoding.UTF8);

			//NSNotificationCenter.DefaultCenter.PostNotificationName("taskModified", this);

			return docData;
		}
	}
}

