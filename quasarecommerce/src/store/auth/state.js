export default function () {
  return {
    authenticateduser: {
      user:{
        id:null,
        firstName:null,
        lastName:null,
        userName:null,
        email:null,
      },
      roles:[]
    },
    loggedIn:false
  };
}
