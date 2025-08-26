import { mount } from '@vue/test-utils';
import HelloWorld from '../../components/HelloWorld.vue';
import { describe, it, expect } from 'vitest';

describe('HelloWorld', () => {
  it('renders properly', () => {
    const wrapper = mount(HelloWorld, { props: { msg: 'Hello Vitest' } });
    expect(wrapper.text()).toContain('Hello Vitest');
  });
});