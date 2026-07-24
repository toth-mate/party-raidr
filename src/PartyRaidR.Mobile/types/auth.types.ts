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

export type UserRole = 'User' | 'Admin';
