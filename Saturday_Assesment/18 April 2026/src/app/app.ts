import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Parentcomponent } from './parentcomponent/parentcomponent';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, Parentcomponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  isLoggedIn: boolean = true;

  users: string[] = ['Pavan', 'Rahul', 'Kiran'];

  role: string = 'admin';

  toggleLogin() {
    this.isLoggedIn = !this.isLoggedIn;
  }

  changeRole(role: string) {
    this.role = role;
  }
  protected readonly title = signal('directivedemo');
}