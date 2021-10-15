using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        // General Messages
        public static string AddedSuccesfully => "Added Successfully";
        public static string UpdatedSuccessfully => "Updated Successfully";
        public static string DeletedSuccessfully => "Deleted Successfully";
        public static string NotFound => "Entity not found";

        //Auth Manager
        public static string UserNameOrPasswordIsIncorrect => "Username or password is incorrect";
        public static string ConfirmYourAccount => "Please confirm your account";
        public static string EmailIsAlreadyExist => "Email is already exist";
        public static string UsernameIsAlreadyExist => "Username is already exist";
        public static string RegisterSuccessfully => "Register successfuly please look at your mail box for account confirmation.";
        public static string UserNotFound => "User not found";
        public static string TokenOrUserNotFound => "Token or User Not Found";
        public static string RefreshTokenNotFound => "Refresh Token Not Found";
        public static string AlreadyAccountConfirmed => "Already your account confirmed";
        public static string SuccessfullyAccountConfirmed => "Account confirmed successfully.You can login now";
        public static string AccountDontConfirmed => "Account dont Confirmed";
        public static string LogoutSuccessfully => "Logout successfully";

        public static string RefreshTokenExpired => "Refresh Token Expired";

        //User Service
        public static string CurrentPasswordIsFalse => "Current password is false";
        public static string PasswordDontMatchWithConfirmation => "Password doesn't match its confirmation";
        public static string PasswordChangedSuccessfully => "Password changed successfully";
        public static string IfEmailTrue => "When you fill in your registered email address, you will be sent instructions on how to reset your password.";
        public static string PasswordResetSuccessfully => "Password has been reset successfully";

        // Order Manager

        public static string YourOrderIsBeingProcessed => "Your order is being processed.";
        public static string PaymentFailed => "Payment failed";

        // Role Manager 

        public static string RoleNameIsAlreadyExist => "Role name is already exist";
        public static string RoleAssignedSuccessfully => "Role assigned successfully";
    }
}
