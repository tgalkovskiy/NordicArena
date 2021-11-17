using Photon.Pun;

public class MonstersView : View
{
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _presenter = new Presenter(this);
    }
}
