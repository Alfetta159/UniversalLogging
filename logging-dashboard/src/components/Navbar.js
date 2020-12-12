import React, { useState } from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  NavbarText
} from 'reactstrap';
import SeverityNavDropDown from './SeverityNavDropDown';

const MeyerNavbar = (props) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <>
       <Navbar color="light" light expand="md">
      <NavbarBrand href="/">Meyer Logging</NavbarBrand>
         <NavbarToggler onClick={toggle} />
       <Collapse isOpen={isOpen} navbar>
         <Nav className="mr-auto" navbar>
         <SeverityNavDropDown />
            { /* <UncontrolledDropdown nav inNavbar>
              <DropdownToggle nav caret>
                Environment
              </DropdownToggle>
              <DropdownMenu right>
                <DropdownItem>
                  All
                </DropdownItem>
                <DropdownItem divider />
                <DropdownItem>
                  Development
                </DropdownItem>
                <DropdownItem>
                  Test
                </DropdownItem>
                <DropdownItem>
                  Production
                </DropdownItem>
           </DropdownMenu>
            </UncontrolledDropdown>*/}
          { /*  
           <UncontrolledDropdown nav inNavbar>
              <DropdownToggle nav caret>
                Applications
              </DropdownToggle>
              <DropdownMenu right>
                <DropdownItem>
                  All
                </DropdownItem>
                <DropdownItem divider />
                <DropdownItem>
                  CER
                </DropdownItem>
                <DropdownItem>
                  Accounts Payable
                </DropdownItem>
                <DropdownItem>
                  EDI
                </DropdownItem>
              </DropdownMenu>
            </UncontrolledDropdown>
            <UncontrolledDropdown nav inNavbar>
              <DropdownToggle nav caret>
                Users
              </DropdownToggle>
              <DropdownMenu right>
                <DropdownItem>
                  All
                </DropdownItem>
                <DropdownItem divider />
                <DropdownItem>
                  CER
                </DropdownItem>
                <DropdownItem>
                  Accounts Payable
                </DropdownItem>
                <DropdownItem>
                  EDI
                </DropdownItem>
              </DropdownMenu>
            </UncontrolledDropdown>
          </Nav>
         <Nav className="mr-auto" navbar>
            <NavItem>
              <UncontrolledDropdown nav inNavbar>
                <DropdownToggle nav caret>
                  Daniel Przybylski
                </DropdownToggle>
                <DropdownMenu right>
                  <DropdownItem>
                    Development
                </DropdownItem>
                  <DropdownItem>
                    Test
                </DropdownItem>
                  <DropdownItem>
                    Production
                </DropdownItem>
                  <DropdownItem divider />
                  <DropdownItem>
                    Reset
                </DropdownItem>
                </DropdownMenu>
              </UncontrolledDropdown>
            </NavItem>
          </Nav>
          <Nav className="mr-auto" navbar>
            <NavItem>
              <NavLink href="/about/">About</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/help/">Help</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/other/">Other Applications</NavLink>
            </NavItem>*/}
          </Nav>
        </Collapse>
      </Navbar>
    </>
  );
}

export default MeyerNavbar;