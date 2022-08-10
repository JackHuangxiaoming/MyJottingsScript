using FairyGUI;

namespace PlatformLobbyCode
{
    public class PopupPanel : GComponent
    {
        private GTextField _title;
        private GTextField _content1;
        private GTextField _content2;
        private GButton _btnClose;
        private GButton _btnOK;
        private GButton _btnCancel;
        private GButton _btnOks;
        private EventCallback0 callbackOk;
        private EventCallback0 callbackCancel;

        public override void ConstructFromXML(FairyGUI.Utils.XML xml)
        {
            _title = this.GetChild("title").asTextField;
            _content1 = this.GetChild("content1").asTextField;
            _content2 = this.GetChild("content2").asTextField;
            _btnClose = this.GetChild("btn_close").asButton;
            _btnClose.onClick.Add(this.OnBtnClose);
            _btnOK = this.GetChild("btnOk").asButton;
            _btnOK.onClick.Add(this.OnBtnOk);
            _btnCancel = this.GetChild("btnCancel").asButton;
            _btnCancel.onClick.Add(this.OnBtnCancel);
            _btnOks = this.GetChild("btnOk1").asButton;
            _btnOks.onClick.Add(this.OnBtnOk);
            _btnOks.visible = false;
        }
        public void SetContent(string title, string content1, string content2, EventCallback0 callback1, EventCallback0 callback2)
        {
            if (title != "" && title != string.Empty)
                _title.text = title;
            else
                _title.text = "提示";
            _content1.text = content1;
            _content2.text = content2;
            callbackOk = callback1;
            callbackCancel = callback2;
            if (callback2 == null)
            {
                _btnOK.visible = false;
                _btnCancel.visible = false;
                _btnOks.visible = true;
                _btnClose.visible = false;
            }
        }
        private void OnBtnClose()
        {
            if (callbackCancel != null) callbackCancel();
            this.Dispose();
        }

        private void OnBtnOk()
        {
            if (callbackOk != null) callbackOk();
            this.Dispose();
        }
        private void OnBtnCancel()
        {
            if (callbackCancel != null) callbackCancel();
            this.Dispose();
        }

    }
}
