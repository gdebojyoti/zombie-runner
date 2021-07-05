public class SampleScript {

  #region public members

    public int publicMember = 0; // public variables
  
  #endregion

  #region private members

    private int m_privateMember = 0; // private variables
  
  #endregion

  #region public methods

    public void PublicMethod (int value) {
      m_privateMember = value;
    }
  
  #endregion

  #region private methods
  
    private void _PrivateMethod () {
      m_privateMember = 0;
    }
  
  #endregion

}