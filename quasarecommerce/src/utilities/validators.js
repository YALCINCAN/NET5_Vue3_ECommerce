//Password Validators

export function oneUpperCase (password) {
  return /[A-Z]/.test(password)
}