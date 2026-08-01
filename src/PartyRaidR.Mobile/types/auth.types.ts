export interface UserLoginDto {
  email: string;
  password: string;
}

export interface UserDto {
  id: string;
  username: string;
  email: string;
  profilePictureUrl: string;
  registerDate: string;
  birtDate: string;
  role: UserRole;
}

export interface UserRegisterDto {
  username: string;
  email: string;
  role: UserRole;
  birthDate: string;
  password: string;
}

export type UserRole = 'User' | 'Admin';
