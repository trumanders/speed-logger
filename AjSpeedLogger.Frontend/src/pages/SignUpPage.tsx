const SignUpPage = () => {
  return (
    <>
      <h1>Skapa konto</h1>
      <div>
        <input type="text" placeholder="E-post" />
      </div>
      <div>
        <input type="password" placeholder="Lösenord" />
      </div>
      <div>
        <input type="password" placeholder="Bekräfta lösenord" />
      </div>

      <button>Skapa konto</button>
    </>
  );
};

export default SignUpPage;
