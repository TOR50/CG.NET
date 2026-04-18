import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

type Country = {
  name: string;
  states: string[];
};

@Component({
  selector: 'app-childcomponent',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './childcomponent.html',
  styleUrls: ['./childcomponent.css'],
})
export class Childcomponent {
  states: string[] = [];
  selectedState: string | null = null;
  private _country: Country | null = null;

  @Input()
  set country(value: Country | null) {
    this._country = value;
    this.states = value?.states ?? [];
    this.selectedState = this.states.length ? this.states[0] : null;
  }

  get country(): Country | null {
    return this._country;
  }

  onStateChange(state: string) {
    this.selectedState = state;
  }
}
