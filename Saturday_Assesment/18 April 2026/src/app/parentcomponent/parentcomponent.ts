import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Childcomponent } from '../childcomponent/childcomponent';

type Country = {
  name: string;
  states: string[];
};

@Component({
  selector: 'app-parentcomponent',
  standalone: true,
  imports: [CommonModule, Childcomponent],
  templateUrl: './parentcomponent.html',
  styleUrls: ['./parentcomponent.css'],
})
export class Parentcomponent {
  countries: Country[] = [
    { name: 'United States', states: ['California', 'Texas', 'New York'] },
    { name: 'Canada', states: ['Ontario', 'Quebec', 'British Columbia'] },
    { name: 'Australia', states: ['New South Wales', 'Queensland', 'Victoria'] },
  ];

  selectedCountry: Country = this.countries[0];

  onCountryChange(countryName: string) {
    const country = this.countries.find((item) => item.name === countryName);
    if (country) {
      this.selectedCountry = country;
    }
  }
}
