public class ApplicationObjectListDC
{
	public int ID {get;set;}
	public int ParentID {get;set;}
	public string NodeText {get;set;}
	public bool Checked {get;set;}
	public int IconNo {get;set;}
	public string Tag {get;set;}

	public ApplicationObjectListDC(int ID_,int ParentID_,string NodeText_,bool Checked_,int IconNo_,string Tag_)
	{
		this.ID = ID_;
		this.ParentID = ParentID_;
		this.NodeText = NodeText_;
		this.Checked = Checked_;
		this.IconNo = IconNo_;
		this.Tag = Tag_;
	}
	public ApplicationObjectListDC(){}
}